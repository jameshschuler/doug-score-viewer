import { countries } from '../constants/countries';
import { Country } from '../models/country';

export function getFlagIcon ( flagIconName: string ): string {
    const url = new URL( `../assets/flags/${flagIconName}.png`, import.meta.url ).href;
    return url;
}

export function getCountry ( countryName: string ): Country {
    const country = countries.find( e => e.name === countryName );
    return country!;
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