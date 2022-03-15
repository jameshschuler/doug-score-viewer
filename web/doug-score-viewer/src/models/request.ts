export interface SearchDougScoreRequest {
    minYear: string;
    maxYear: string;
    model?: string;
    make?: string;
    originCountries: string[]
}