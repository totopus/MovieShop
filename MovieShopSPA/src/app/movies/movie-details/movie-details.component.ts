import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { Movie } from 'src/app/shared/models/movie';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  movie!: Movie;
  id: number = 0;

  constructor(private activeRoute: ActivatedRoute, private movieService: MovieService) { }

  ngOnInit(): void {
    // ActivatedRoute => that will give us all the information of the current route/url
    // get the id from the URL 1, 2
    // then call our api, getMovieDetails method
    this.activeRoute.paramMap.subscribe
      (
        p => {
          this.id = Number(p.get('id'));
          console.log('movieId= ' + this.id);
          // call the api
          this.movieService.getMovieDetailById(this.id)
            .subscribe(
              m => {
                this.movie = m;
                console.log(this.movie);
              }
            );
        }
      )
    console.log('inside movie details component');
  }

}