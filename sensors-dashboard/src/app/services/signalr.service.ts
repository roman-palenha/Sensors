import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { Sensor } from '../models/sensor.model';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection: signalR.HubConnection;
  private sensorDataSubject = new Subject<Sensor[]>();

  sensorData$ = this.sensorDataSubject.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.baseUrl}/sensorHub`)
      .build();

    this.hubConnection.on('ReceiveSensorData', (data : any) => {
      this.sensorDataSubject.next(data);
    });

    this.hubConnection.start().catch(err => console.error(err.toString()));
  }
}
