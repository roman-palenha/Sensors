import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { FishCount } from '../models/fish-count.model';
import { WaterTemp } from '../models/water-temp.model';

@Injectable({
  providedIn: 'root'
})
export class SensorService {
  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getTotalTemperatureAverage(startDate: Date, endDate: Date): Observable<WaterTemp> {
    const params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString());
      
    return this.http.get<WaterTemp>(`${this.baseUrl}/total/temperature/average`, { params });
  }

  getTopFishSpecies(n: number, startDate: Date, endDate: Date): Observable<FishCount[]> {
    const params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString());

    return this.http.get<any[]>(`${this.baseUrl}/total/fish/top/${n}`, { params });
  }

  getGroupTemperatureAverage(groupName: string, startDate: Date, endDate: Date): Observable<WaterTemp> {
    const params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString());

    return this.http.get<WaterTemp>(`${this.baseUrl}/group/${groupName}/temperature/average`, { params });
  }

  getGroupTopFishSpecies(groupName: string, n: number, startDate: Date, endDate: Date): Observable<FishCount[]> {
    const params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString());

    return this.http.get<any[]>(`${this.baseUrl}/group/${groupName}/fish/top/${n}`, { params });
  }
}
