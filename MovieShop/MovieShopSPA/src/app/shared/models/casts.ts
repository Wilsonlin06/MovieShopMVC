import { Movie } from "./movieDetails";

export interface Cast{
    id : number;
    name : string;
    gender : string;
    profilePath : string;
    character : string;
    movies : Movie[];
}