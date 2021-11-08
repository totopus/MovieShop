import { Component,Input, OnInit } from '@angular/core';
import { GenreService } from '../core/services/genre.service';
import { Genre } from '../shared/models/movie';
@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.css']
})
export class GenresComponent implements OnInit {

  constructor(private genreService:GenreService) { }
  @Input() genres!:Genre[];
  
  ngOnInit(): void {
    this.genreService.getAllGenres().subscribe(
      g=>{
        this.genres = g;
        console.table(this.genres);
      }
    )
  }

}
