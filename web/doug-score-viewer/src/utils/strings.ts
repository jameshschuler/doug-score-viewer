export function isNullEmptyOrWhitespace ( value?: any ) {
    return value === undefined || value === null || value === '' || value === ' ';
}