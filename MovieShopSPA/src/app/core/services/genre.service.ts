import { Injectable } from '@angular/core';
import { Genre } from 'src/app/shared/models/movie';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private http:HttpClient) { }
  
  getAllGenres():Observable<Genre[]>
  {
    return this.http.get<Genre[]>('https://localhost:44384/api/Genres')
  }
}
