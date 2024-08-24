using System;
using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;

namespace Bongo.Core
{
	[TestFixture]
	public class StudyRoomBookingServiceTests
	{
		private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
		private Mock<IStudyRoomRepository> _studyRoomRepoMock;
		private StudyRoomBookingService _bookingService;

		private StudyRoomBooking _request;
		private List<StudyRoom> _availiableStudyRoom;

		[SetUp]
		public void SetUp()
		{
			_request = new StudyRoomBooking() {
				FirstName = "Ben",
				LastName = "Spark",
				Email = "ben@gmail.com",
				Date = new DateTime(2024,2,2)
			};

			_availiableStudyRoom = new List<StudyRoom>()
			{
				new StudyRoom()
				{
					Id = 10,
					RoomName = "Michigan",
					RoomNumber = "A202"
				}
			};

			_studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
			_studyRoomRepoMock = new Mock<IStudyRoomRepository>();
			_bookingService = new StudyRoomBookingService(
				_studyRoomBookingRepoMock.Object,
				_studyRoomRepoMock.Object
			);
			_studyRoomRepoMock.Setup(x => x.GetAll()).Returns(_availiableStudyRoom);
		}

		[TestCase]
		public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
		{
			_bookingService.GetAllBooking();
			_studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
		}

		[TestCase]
		public void BookingException_NullRequest_ThrowsExxception()
		{
			var exception = Assert.Throws<ArgumentNullException>(
				() => _bookingService.BookStudyRoom(null));
			Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'request')"));
			Assert.That(exception.ParamName,Is.EqualTo("request"));
		}

		[Test]
		public void StudyRoomBooking_SaveBookingWithAvaialiableRoom_ReturnsResultWithAllValues()
		{
			StudyRoomBooking savedStudyRoomBooking = null;
			_studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
				.Callback<StudyRoomBooking>(booking =>
				{
					savedStudyRoomBooking = booking;
				});
			_bookingService.BookStudyRoom(_request);

			_studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

			Assert.That(savedStudyRoomBooking, Is.Not.Null);
			Assert.That(savedStudyRoomBooking.FirstName, Is.EqualTo(_request.FirstName));
			Assert.That(savedStudyRoomBooking.LastName, Is.EqualTo(_request.LastName));
			Assert.That(savedStudyRoomBooking.Email, Is.EqualTo(_request.Email));
			Assert.That(savedStudyRoomBooking.Date, Is.EqualTo(_request.Date));
			Assert.That(_availiableStudyRoom.First().Id, Is.EqualTo(savedStudyRoomBooking.StudyRoomId));


		}
		[Test]
		public void StudyRoomBookingResultCheck_InputRequest_ValuesMatchInResult()
		{
			StudyRoomBookingResult result = _bookingService.BookStudyRoom(_request);
			Assert.NotNull(result);
			Assert.That(result.FirstName, Is.EqualTo(_request.FirstName));
			Assert.That(result.LastName, Is.EqualTo(_request.LastName));
			Assert.That(result.Email, Is.EqualTo(_request.Email));
			Assert.That(result.Date, Is.EqualTo(_request.Date));
		}


		[TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
		[TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
		public StudyRoomBookingCode ResultCodeSuccess_RoomAvaility_ReturnsSuccessResultCode(bool roomAvailiablity) {
			if (!roomAvailiablity)
			{
				_availiableStudyRoom.Clear();
			}
			return _bookingService.BookStudyRoom(_request).Code;
		}


        [TestCase(0, false)]
		[TestCase(55, true)]
        public void StudyRoomBooking_BookRoomWithAvailability_ReturnsBookingId(
			int expectedBookingId,
			bool roomAvailability
			)
        {
			if (!roomAvailability)
			{
				_availiableStudyRoom.Clear();
			}
            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    booking.BookingId = 55;
                });
            var result = _bookingService.BookStudyRoom(_request);

 

			Assert.That( expectedBookingId, Is.EqualTo(result.BookingId));


        }

		[Test]
        public void BookNotInvoked_SaveBookingWithoutAvailableRoom_BookMethodNotInvoked()
        {
				_availiableStudyRoom.Clear();

         
            var result = _bookingService.BookStudyRoom(_request);

			_studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }


    }
}

