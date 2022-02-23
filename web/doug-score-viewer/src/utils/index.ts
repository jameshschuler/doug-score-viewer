export function getFlagIcon ( originCountry: string ): string {
    const fileName = originCountry.toLowerCase().replace( " ", "-" );
    return `assets/${fileName}.png`;
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

export function getScoreBracket ( score: number ): string {
    let result;
    if ( score >= 30 ) {
        result = 'has-border-great';
    } else if ( score >= 20 ) {
        result = 'has-border-okay';
    } else {
        result = 'has-border-sad';
    }

    return result;
}