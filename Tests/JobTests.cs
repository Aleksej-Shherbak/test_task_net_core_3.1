using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domains;
using Moq;
using NUnit.Framework;
using Repos.Abstract;
using WebApi.Automapper.Profiles;
using WebApi.Controllers;
using WebApi.Requests;

namespace Tests
{
    public class Tests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<JobToJobResponseProfile>();
                opts.AddProfile<JobRequestToJobProfile>();
                opts.AddProfile<JobPagedListToJobItemResponsePagedList>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task UserCanGetJobsWithPaging()
        {
            var queryableJobs = Enumerable.Range(1, 10).Select(x => new Job
            {
                JobId = x,
                Title = $"Job {x}",
                Description = $"This is job {x}"
            }).AsQueryable();

            var mock = new Mock<IJobRepository>();

            mock.Setup(x => x.All).Returns(queryableJobs);

            var controller = new JobsController(mock.Object, _mapper);

            var actionResult = await controller.Index(2);
            var res = actionResult.Value;

            Assert.NotNull(res);
            Assert.AreEqual(10, res.TotalItemCount);
            Assert.AreEqual(2, res.PageCount);
            Assert.AreEqual(2, res.PageNumber);
        }

        [Test]
        public async Task CanSaveValidChanges()
        {
            //Arrange
            var mock = new Mock<IJobRepository>();
            JobsController controller = new JobsController(mock.Object, _mapper);

            var job = new JobRequest {Title = "Test", Description = "Test"};

            //Act
            var actionResult = await controller.Create(job);

            //Assert
            mock.Verify(m => m.SaveAsync(It.IsAny<Job>()));
        }

        [Test]
        public async Task CanNotSaveInvalidChanges()
        {
            //Arrange
            var mock = new Mock<IJobRepository>();
            JobsController controller = new JobsController(mock.Object, _mapper);

            // see, without Description field, just only Title ... 
            var job = new JobRequest {Title = "Test"};

            //Act
            var actionResult = await controller.Create(job);

            //Assert
            mock.Verify(m => m.SaveAsync(It.IsAny<Job>()));
        }
        
        [Test]
        public async Task CanDeleteJob()
        {
            //Arrange
            var queryableJobs = Enumerable.Range(1, 10).Select(x => new Job
            {
                JobId = x,
                Title = $"Job {x}",
                Description = $"This is job {x}"
            }).AsQueryable();

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(default);

            var mock = new Mock<IJobRepository>();
            mock.Setup(x => x.DeleteAsync(It.IsAny<int>())).Returns(() => tcs.Task);
            mock.Setup(x => x.All).Returns(queryableJobs);
            JobsController controller = new JobsController(mock.Object, _mapper);

            //Act
            var actionResult = await controller.Delete(1);

            //Assert
            mock.Verify(m => m.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}