/**
 * Danfoss TetTask Service API
 * Test Task
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { House } from './house';

export interface Counter { 
    id?: number;
    serialNumber?: string;
    value?: number;
    house?: House;
    houseId?: number;
}