using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Services;
using Moq;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
        private MovieService _sut;
        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;

        [TestInitialize]
        // [OneTimeSetUp] in nUnit
        public void OneTimeSetup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();

            // system under test MovieService => GetTop30GrossingMovies
            _sut = new MovieService(_mockMovieRepository.Object);

            // for mock movierepository, whenever someone call GetTop30GrossingMovies, it will return this _movies object.
            _mockMovieRepository.Setup(expression: m => m.GetTop30GrossingMovies()).ReturnsAsync(_movies);
        }

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            _movies = new List<Movie>()
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
            };
           
        }
 

        [TestMethod]
        public async Task TestListOfHighestGrossingMoviesFromFakeData()
        {
            // system under test(SUT) MovieService => GetTop30GrossingMovies

            // _sut = new MovieService(new MockMovieRepository());

            var movies = await _sut.GetTop30GrossingMovies();

            // check the actual output with expected data.

            // Arrange, Act and Assert
            Assert.IsNotNull(movies);
            Assert.IsInstanceOfType(movies, typeof(IEnumerable<MovieCardModel>));
            Assert.AreEqual(16, movies.Count());
        }
    }

    public class MockMovieRepository : IMovieRepository
    {
        public Task<Movie> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetTop30GrossingMovies()
        {
            // this method will fake the data.

            var _movies = new List<Movie>()
            {
            new Movie { Id=1, Title="Inception", PosterUrl="https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg" },
            new Movie { Id=2, Title="Interstellar", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg" },
            new Movie { Id=3, Title="The Dark Knight", PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg" },
            new Movie { Id=4, Title="Deadpool", PosterUrl="https://image.tmdb.org/t/p/w342//yGSxMiF0cYuAiyuve5DA6bnWEOI.jpg" },
            new Movie { Id=5, Title="The Avengers", PosterUrl="https://image.tmdb.org/t/p/w342//RYMX2wcKCBAr24UyPD7xwmjaTn.jpg" },
            new Movie { Id=6, Title="Avatar", PosterUrl="https://image.tmdb.org/t/p/w342//6EiRUJpuoeQPghrs3YNktfnqOVh.jpg" },
            new Movie { Id=7, Title="Guardians of the Galaxy", PosterUrl="https://image.tmdb.org/t/p/w342//r7vmZjiyZw9rpJMQJdXpjgiCOk9.jpg" },
            new Movie { Id=8, Title="Fight Club", PosterUrl="https://image.tmdb.org/t/p/w342//8kNruSfhk5IoE4eZOc4UpvDn6tq.jpg" },
            new Movie { Id=9, Title="Avengers: Infinity War", PosterUrl="https://image.tmdb.org/t/p/w342//7WsyChQLEftFiDOVTGkv3hFpyyt.jpg" },
            new Movie { Id=10, Title="Pulp Fiction", PosterUrl="https://image.tmdb.org/t/p/w342//plnlrtBUULT0rh3Xsjmpubiso3L.jpg" },
            new Movie { Id=11, Title="Django Unchained", PosterUrl="https://image.tmdb.org/t/p/w342//7oWY8VDWW7thTzWh3OKYRkWUlD5.jpg" },
            new Movie { Id=12, Title="Iron Man", PosterUrl="https://image.tmdb.org/t/p/w342//78lPtwv72eTNqFW9COBYI0dWDJa.jpg" }
            };

            return _movies;
        }
    }
}