import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {MovieCard} from 'src/app/shared/models/movieCard';
import { Movie } from 'src/app/shared/models/movieDetails';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  // should have all the methods that deals with Movies, getById, getTopRevenue, getTopRated
  // HttpClient to make AJAX request
  // XMLHttpRequest => 
  constructor(private http: HttpClient) { }

  getTopRevenueMovies() : Observable<MovieCard[]>{
    // https://localhost:44396/api/Movies/toprevenue
    // model based on JSON data
    // Observables are Lazy, only when you subscribe to an observable you will get the data
    // Youtube => channels
    // XBox => videos
    // notification when Xbox posts a new video
    // subscribe it
    // HttpClient in Angular => Observable => 
    // localhost:4200 (browser) => AJAX call to 'https://localhost:44396/api/Movies/toprevenue'
    // yahoo.com => google.com

    return this.http.get(`${environment.apiUrl}`+'Movies/toprevenue')
    .pipe(
      map( resp => resp as MovieCard[])
    )
    // in C#: movies.where(m => m.bugget > 10000).select( m => new MovieCard { id = m.id, title = m.title}).ToList()
    
  }

  getMovieDetails(movieId: number): Observable<Movie>{
    return this.http.get(`${environment.apiUrl}`+`Movies/${movieId}`)
    .pipe(
      map( resp => resp as Movie)
    )
  }
}
