import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CastDetailsComponent } from './cast-details/cast-details.component';
import { MovieDetailsComponent } from './movie-details/movie-details.component';
import { MoviesComponent } from './movies.component';
import { TopratedComponent } from './toprated/toprated.component';

const routes: Routes = [

  // localhost:4200/movies
  {
    path: '', component: MoviesComponent,
    children: [
      {
        path: ':id', component: MovieDetailsComponent
      },
      {
        path: 'cast/:id', component: CastDetailsComponent
      },
      {
        path: 'toprated', component: TopratedComponent
      },
     
    ]

  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MoviesRoutingModule { }