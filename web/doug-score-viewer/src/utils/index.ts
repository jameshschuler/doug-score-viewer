export function getFlagIcon ( originCountry: string ): string {
    const fileName = originCountry.toLowerCase().replace( " ", "-" );
    return `src/assets/${fileName}.png`;
}

export function getDougScoreBracket ( dougScore: number ): string {
    if ( dougScore >= 65 ) {
        return 'has-border-glad';
    } else if ( dougScore > 50 ) {
        return 'has-border-okay';
    } else {
        return 'has-border-sad';
    }
}

// TODO:
export function getDailyScoreBracket ( dailyScore: number ): string {
    return '';
}

// TODO:
export function getWeekendScoreBracket ( weekendScore: number ): string {
    return '';
}