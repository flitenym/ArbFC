
export interface IExchangeChain {
    id: number | null;
    name: string | null;
    order?: number;
}


export interface IExchangeChainTickers {
    fromTicker: string | null;
    toTicker: string | null;
}