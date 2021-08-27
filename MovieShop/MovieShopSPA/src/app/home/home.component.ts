import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/movieCard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  movies!: MovieCard[];

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    // right event to call our API    
    // location.reload;
    this.movieService.getTopRevenueMovies().subscribe(
      m => {
        this.movies = m;
        console.log('Inside home componenet init method');
        console.table(this.movies);
      }
    );
  }

  // Angular Life Cycle Hooks, go through certain events
  // Person life cycle: birthday, school, marrage, death

}
