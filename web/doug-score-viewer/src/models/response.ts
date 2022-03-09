import { Option } from './common';
import { DougScoreResponse } from './dougScore';

export interface FeaturedDougScoresResponse {
    dougScores: Array<DougScoreResponse>;
}

export interface SearchDougScoresResponse {
    dougScores: Array<DougScoreResponse>;
    currentCount: number;
    currentPage: number;
    totalCount: number;
    totalPageCount: number;
}

export interface AvailableMakesResponse {
    makes: AvailableMake[]
}

export interface AvailableModelsResponse {
    models: AvailableModel[]
}

export interface AvailableModel {
    id: number;
    name: string;
}

export interface AvailableMake {
    count: number;
    name: string;
}

export interface OptionsResponse {
    options?: Option[]
}