import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HomeComponent } from './home/home.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { FavoritesComponent } from './user/favorites/favorites.component';
import { PurhcasesComponent } from './user/purhcases/purhcases.component';

const routes: Routes = [
  {path:"", component: HomeComponent},
  {path:"account/login", component: LoginComponent},
  {path:"account/register", component: RegisterComponent},
  {path:"movies/:id", component: MovieDetailsComponent},
  {path:"user/:id/favorites", component: FavoritesComponent},
  {path:"user/:id/purchases", component: PurhcasesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
