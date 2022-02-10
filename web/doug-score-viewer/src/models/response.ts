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