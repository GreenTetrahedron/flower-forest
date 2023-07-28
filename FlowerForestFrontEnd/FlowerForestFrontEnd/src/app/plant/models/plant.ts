export interface Plant {
    id: string;
    genus: string;
    species: string;
    commonName?: string;
    photoUrl: string;
    maxHeight_metres: number;
    catalogueId: string;
}