import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GenresComponent } from './genres/genres.component';
import { HomeComponent } from './home/home.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';


//specify all the routes required by the angular application

const routes: Routes = [
  //path/route for my home page http://localhost:4200/
  {path:"",component:HomeComponent},
  //lazily load the modules, define main route for
  //lazy modules
  {
    path: "movies", loadChildren: () => import("./movies/movies.module").then(mod => mod.MoviesModule)
  }
  //{path:"movies/toprated", component:HomeComponent},
  //{path: 'movie/details/303',component: MovieDetailsComponent},
  //{path:"genres",component:GenresComponent}
  //{path:"admin/createmovie",component:CreateMovieComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
