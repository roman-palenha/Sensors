import { FishCount } from "./fish-count.model";

export interface Sensor {
    groupName: string;
    waterTemperature: number;
    fishSpecies: FishCount[];
}
