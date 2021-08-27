import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';

@Component({
  selector: 'app-purhcases',
  templateUrl: './purhcases.component.html',
  styleUrls: ['./purhcases.component.css']
})
export class PurhcasesComponent implements OnInit {

  movies!:MovieCard[]
  id!:number
  
  constructor(private userService:UserService, private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentRoute.params.subscribe(
      f =>{
        this.id = f['id'];
      });
    this.userService.getPurchaseMovies(this.id).subscribe(
      p => {
        this.movies = p;
      }
    );
  }

}
