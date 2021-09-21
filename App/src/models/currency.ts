export enum CurrencyType {
    EUR,
    USD
}

export enum CurrencySymbol {
    EUR = "€",
    USD = "$"
}

export interface Currency {
    type: CurrencyType,
    symbol: CurrencySymbol,
    rate?: number
}