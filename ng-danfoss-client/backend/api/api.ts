export * from './counter.service';
import { CounterService } from './counter.service';
export * from './filter.service';
import { FilterService } from './filter.service';
export * from './house.service';
import { HouseService } from './house.service';
export const APIS = [CounterService, FilterService, HouseService];
