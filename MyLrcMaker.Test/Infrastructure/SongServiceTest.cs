using System;
using System.Windows;
using Moq;
using MyLrcMaker.Infrastructure;
using NUnit.Framework;

namespace MyLrcMaker.Test.Infrastructure
{
    [TestFixture]
    public class SongServiceTest : TestBase<SongService>
    {
        [SetUp]
        public void SetUp()
        {
            _mediaElementHostMock.Setup(x => x.NaturalDuration).Returns(new Duration(_mockSongLength));

            ObjectUderTest = new SongService();
            ObjectUderTest.Initialize(_mediaElementHostMock.Object);
        }

        [Test]
        public void ShouldTrowExceptionIfSongServiceIsNotInitialized()
        {
            // prepare 
            ObjectUderTest = new SongService();
            // act & assert
            Assert.Throws<NullReferenceException>(() => ObjectUderTest.Play());
        }

        [Test]
        public void ShouldUpdateTotalLengthWhenSongLoaded()
        {
            // act 
            _mediaElementHostMock.Raise(x => x.MediaOpened += null, new RoutedEventArgs());

            // assert
            Assert.That(ObjectUderTest.TotalLength, Is.EqualTo(_mockSongLength.TotalMilliseconds));
        }

        [Test]
        public void CurrentShouldEqualToTotalWhenSongEnded()
        {
            // act 
            _mediaElementHostMock.Raise(x => x.MediaEnded += null, new RoutedEventArgs());

            // assert
            Assert.That(ObjectUderTest.Current, Is.EqualTo(_mockSongLength.TotalMilliseconds));
        }

        private readonly Mock<IMediaElementHost> _mediaElementHostMock = new Mock<IMediaElementHost>();
        private readonly TimeSpan _mockSongLength = new TimeSpan(0, 0, 3, 25, 123);
    }
}
