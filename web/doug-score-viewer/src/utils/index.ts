export function getFlagIcon ( originCountry: string ): string {
    const fileName = originCountry.toLowerCase().replace( " ", "-" );
    return `src/assets/${fileName}.png`;
}

export function getDougScoreBracket ( dougScore: number ): string {
    if ( dougScore >= 65 ) {
        return 'has-border-great';
    } else if ( dougScore > 50 ) {
        return 'has-border-okay';
    } else {
        return 'has-border-sad';
    }
}

export function getDailyScoreBracket ( dailyScore: number ): string {
    let result;
    if ( dailyScore >= 30 ) {
        result = 'has-border-great';
    } else if ( dailyScore >= 20 ) {
        result = 'has-border-okay';
    } else {
        result = 'has-border-sad';
    }

    return result;
}

// TODO:
export function getWeekendScoreBracket ( weekendScore: number ): string {
    return '';
}