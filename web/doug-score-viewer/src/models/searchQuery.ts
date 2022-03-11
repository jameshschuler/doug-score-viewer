export interface SearchQuery {
    minYear: string;
    maxYear: string;
    model?: string;
    make?: string;
    originCountries: Country[]
}

export interface Country {
    name: string;
    selected: boolean;
    icon: string;
}