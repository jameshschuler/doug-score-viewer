export function isNullEmptyOrWhitespace ( value?: string | null ) {
    return value === undefined || value === null || value === '' || value === ' ';
}