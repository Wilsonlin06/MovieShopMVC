import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {

  movies!: MovieCard[];
  id!: number;

  constructor(private userService: UserService, private currentRoute: ActivatedRoute) { }


  ngOnInit(): void {
    // right event to call our API
    this.currentRoute.params.subscribe(
      f =>{
        this.id = f['id'];
      });
    this.userService.getFavoriteMovies(this.id).subscribe(
      f => {
        this.movies = f;
      }
    );
  }
}
