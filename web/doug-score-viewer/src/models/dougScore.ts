export interface DougScoreResponse {
    filmingLocation: FilmingLocation;
    vehicle: Vehicle;
    dailyScore: DailyScore;
    weekendScore: WeekendScore;
    videoLink: string;
    totalDougScore: number;
    id: number;
}

export interface FilmingLocation {
    city: string;
    state: string;
}

export interface Vehicle {
    make: string;
    model: string;
    originCountry: string;
    year: string;
    id: number;
}

export interface DailyScore {
    comfort: number;
    features: number;
    practicality: number;
    quality: number;
    value: number;
    total: number;
    id: number;
}

export interface WeekendScore {
    acceleration: number;
    coolFactor: number;
    funFactor: number;
    handling: number;
    styling: number;
    total: number;
    id: number;
}