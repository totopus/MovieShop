import { Component, OnInit } from '@angular/core';
import { MovieCard } from 'src/app/shared/models/moviecard';
import { MovieCardComponent } from 'src/app/shared/components/movie-card/movie-card.component';
import { MovieService } from 'src/app/core/services/movie.service';

@Component({
  selector: 'app-toprated',
  templateUrl: './toprated.component.html',
  styleUrls: ['./toprated.component.css']
})
export class TopratedComponent implements OnInit {

  movieCards!: MovieCard[];
  constructor(private movieService: MovieService) { }

  ngOnInit(): void {

     this.movieService.getTopRatedMovies().subscribe(
      m=>{
        this.movieCards = m;
        console.log('inside the ngOnInit method of Home Component');
        
        //to print array items in console window use console.table
        console.table(this.movieCards);
      }
    );
  }

}
