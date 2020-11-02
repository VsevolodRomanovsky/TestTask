export * from './counter.service';
import { CounterService } from './counter.service';
export * from './house.service';
import { HouseService } from './house.service';
export const APIS = [CounterService, HouseService];
