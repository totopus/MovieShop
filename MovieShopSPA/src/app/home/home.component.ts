import { Component,OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/moviecard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  //
  movieTitle="Movie Shop SPA"
  movieCards!: MovieCard[];
  constructor(private movieService:MovieService) { }

  ngOnInit(): void {
    //ngOnInit is one of the most important life cycle
    //hooks method in angular
    //it is recommended to use this method to call the 
    //API and initialize any data properties
    //this method will be called automatically by your
    //angular component after calling constructor
    
    //only when you subscribe to the observable you get
    //the data
    //observable<MovieCard[]>
    //http://localhost:4200/ => HomeComponent
    this.movieService.getTopRevenueMovies().subscribe(
      m=>{


        this.movieCards = m;
        console.log('inside the ngOnInit method of Home Component');
        
        //to print array items in console window use console.table
        console.table(this.movieCards);
      });


   

    
  }

}
