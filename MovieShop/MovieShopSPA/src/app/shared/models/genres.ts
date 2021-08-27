import { Movie } from "./movieDetails";

export interface Genre{
    id : number;
    name : string;

    movies : Movie[];
}