import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { Movie } from 'src/app/shared/models/movieDetails';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  
  bgUrl!:string;
  movieId!:number;
  movie!: Movie;
  constructor(private movieService:MovieService, private currentRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.currentRoute.params.subscribe(
      f =>{
        this.movieId = f['id'];
      });
    this.movieService.getMovieDetails(this.movieId).subscribe(
      m => {
        this.movie = m;
        console.table(this.movie);
      }
    );
  }

}
