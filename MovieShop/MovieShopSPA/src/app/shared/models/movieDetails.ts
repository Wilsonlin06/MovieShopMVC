import { Cast } from "./casts";
import { Genre } from "./genres";

export interface Movie{
    id : number;
    title : string;
    overview : string;
    tagline : string;
    budget : number;
    revenue : number;
    imdbUrl : string;
    tmdbUrl : string;
    posterUrl : string;
    backdropUrl : string;
    originalLanguage : string;
    releaseDate : Date;
    runTime : number;
    price : number;
    rating : number;

    casts : Cast[];
    genres: Genre[];
}