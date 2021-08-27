using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.GetMovies();
            var movieList = new List<MovieCardResponseModel>();
            foreach(var movie in movies)
            {
                movieList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            return movieList;
        }

        public async Task<List<MovieCardResponseModel>> GetMovieByGenreId(int genreId)
        {
            var genres = await _movieRepository.GetMoviesByGenreId(genreId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach(var genre in genres)
            {
                foreach(var movie in genre.Movies)
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            };
            return movieCards;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            var movieDetailsModel = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price
            };

            movieDetailsModel.Casts = new List<CastResponseModel>();

            foreach (var cast in movie.MovieCasts)
            {
                movieDetailsModel.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Character = cast.Character,
                    ProfilePath = cast.Cast.ProfilePath,
                    Gender = cast.Cast.Gender
                });
            }

            movieDetailsModel.Genres = new List<GenreResponseModel>();

            foreach (var genre in movie.Genres)
            {
                movieDetailsModel.Genres.Add(new GenreResponseModel 
                {
                    Id = genre.Id,
                    Name = genre.Name                    
                });
            }

            return movieDetailsModel;
            //var movieDetails = new List<MovieDetailsResponseModel>();
            //movieDetails.Add(new MovieDetailsResponseModel
            //{
            //    Id = movieDetailsModel.Id,
            //    Title = movieDetailsModel.Title,
            //    Overview = movieDetailsModel.Overview,
            //    Tagline = movieDetailsModel.Tagline,
            //    Budget = movieDetailsModel.Budget,
            //    Revenue = movieDetailsModel.Revenue,
            //    ImdbUrl = movieDetailsModel.ImdbUrl,
            //    TmdbUrl = movieDetailsModel.TmdbUrl,
            //    PosterUrl = movieDetailsModel.PosterUrl,
            //    BackdropUrl = movieDetailsModel.BackdropUrl,
            //    OriginalLanguage = movieDetailsModel.OriginalLanguage,
            //    ReleaseDate = movieDetailsModel.ReleaseDate,
            //    RunTime = movieDetailsModel.RunTime,
            //    Price = movieDetailsModel.Price,
            //    Rating = movieDetailsModel.Rating
            //});

            //return movieDetails;
        }

        public async Task<List<ReviewRequestModel>> GetReviewByMovieId(int movieId)
        {
            var reviews = await _movieRepository.GetReviewByMovieId(movieId);
            var reviewDetails = new List<ReviewRequestModel>();
            foreach(var review in reviews)
            {
                reviewDetails.Add(new ReviewRequestModel
                {
                    movieId = review.MovieId,
                    userId = review.UserId,
                    Rating = (decimal)review.Rating,
                    reviewText = review.ReviewText
                });
            };
            return reviewDetails;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.Get30HighestRatedMovies();
            var movieCards = new List<MovieCardResponseModel>();

            foreach(var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            // call repositories and get the real data from database
            // call the movie repository class
            var movies = await _movieRepository.Get30HighestRevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel 
                { 
                    Id = movie.Id, 
                    Title = movie.Title, 
                    PosterUrl = movie.PosterUrl
                });
            }

            return movieCards;
        }
    }
}
