import { Option } from '../models/common';

export enum SortBy {
    TotalDougScoreDesc = 'TotalDougScore|desc',
    TotalDougScoreAsc = 'TotalDougScore|asc',
    DailyScoreDesc = 'DailyScore|desc',
    DailyScoreAsc = 'DailyScore|asc',
    WeekendScoreDesc = 'WeekendScore|desc',
    WeekendScoreAsc = 'WeekendScore|asc',
    YearDesc = 'Year|desc',
    YearAsc = 'Year|asc'
}

export const sortByOptions: Option[] = [
    { value: SortBy.TotalDougScoreDesc, text: 'Total DougScore (descending)' },
    { value: SortBy.TotalDougScoreAsc, text: 'Total DougScore (ascending)' },
    { value: SortBy.DailyScoreDesc, text: 'Daily Score (descending)' },
    { value: SortBy.DailyScoreAsc, text: 'Daily Score (ascending)' },
    { value: SortBy.WeekendScoreDesc, text: 'Weekend Score (descending)' },
    { value: SortBy.WeekendScoreAsc, text: 'Weekend Score (ascending)' },
    { value: SortBy.YearDesc, text: 'Year (descending)' },
    { value: SortBy.YearAsc, text: 'Year (ascending)' }
];