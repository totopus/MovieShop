import { Component, Input,OnInit } from '@angular/core';
import { MovieCard } from '../../models/moviecard';


@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {

  //will receive data from parent component through @input
  @Input() movieCard!:MovieCard;
  constructor() { }

  ngOnInit(): void {
  }

}
