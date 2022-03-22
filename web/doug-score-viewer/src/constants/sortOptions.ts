import { Option } from '../models/common';

export const TotalDougScoreDesc = 'TotalDougScore|desc';
export const TotalDougScoreAsc = 'TotalDougScore|asc';

export const DailyScoreDesc = 'DailyScore|desc';
export const DailyScoreAsc = 'DailyScore|asc';

export const WeekendScoreDesc = 'WeekendScore|desc';
export const WeekendScoreAsc = 'WeekendScore|asc';

export const YearDesc = 'Year|desc';
export const YearAsc = 'Year|asc';

export const sortByOptions: Option[] = [
    { value: TotalDougScoreDesc, text: 'Total DougScore (descending)' },
    { value: TotalDougScoreAsc, text: 'Total DougScore (ascending)' },
    { value: DailyScoreDesc, text: 'Daily Score (descending)' },
    { value: DailyScoreAsc, text: 'Daily Score (ascending)' },
    { value: WeekendScoreDesc, text: 'Weekend Score (descending)' },
    { value: WeekendScoreAsc, text: 'Weekend Score (ascending)' },
    { value: YearDesc, text: 'Year (descending)' },
    { value: YearAsc, text: 'Year (ascending)' }
];