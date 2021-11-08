import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {MovieCard} from 'src/app/shared/models/moviecard';
import {HttpClient} from '@angular/common/http';
import { Movie } from 'src/app/shared/models/movie';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  //private readonly HttpClient _http;
  constructor(private http:HttpClient) { }

  //https://localhost:44384/api/Movies/toprevenue

  //many methods that will be used by components
  //HomeComponent will call this function
  

  getTopRevenueMovies(): Observable<MovieCard[]> {
      // call our API, using HttpClient (XMLHttpRequest) to make GET request
      // HttpClient class comes from HttpClientModule (Angular Team created for us to use)
      // import HttpClientModule inside AppModule
  
      // read the base API Url from the environment file and then append the needed URL per method
      return this.http.get<MovieCard[]>(`${environment.apiBaseURL}movies/toprevenue`);
  
    }
  
    //
    getMovieDetailById(id: number): Observable<Movie> {
      // appsetting.json 
      // https://localhost:5001/api/movies/3
      return this.http.get<Movie>(`${environment.apiBaseURL}movies/${id}`);
    }
  
  getTopRatedMovies():Observable<MovieCard[]>{
    return this.http.get<MovieCard[]>('https://localhost:44384/api/Movies/toprating');
  }

  


}
