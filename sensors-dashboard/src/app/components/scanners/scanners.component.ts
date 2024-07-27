import { Component, OnInit } from '@angular/core';
import { Sensor } from 'src/app/models/sensor.model';
import { SignalrService } from '../../services/signalr.service';

@Component({
  selector: 'app-scanners',
  templateUrl: './scanners.component.html',
  styleUrls: ['./scanners.component.css']
})
export class ScannersComponent implements OnInit {
  groups = ['alpha', 'beta', 'gamma', 'delta', 'epsilon', 'zeta', 'eta', 'theta', 'iota'];
  selectedGroup?: string;
  sensors : Sensor[] = [];
  allSensors : Sensor[] = [];

  constructor(private signalrService: SignalrService) { }

  ngOnInit(): void {
    this.signalrService.sensorData$.subscribe((data: Sensor[]) => {
      this.allSensors = data;
      if (this.selectedGroup) {
        this.sensors = data.filter(sensor => sensor.groupName.startsWith(this.selectedGroup!));
      }
    });
  }

  selectGroup(groupName: string): void {
    this.selectedGroup = groupName;
    this.sensors = this.allSensors.filter(sensor => sensor.groupName.startsWith(this.selectedGroup!));
  }

  backToGroups(): void {
    this.selectedGroup = undefined;
    this.sensors = [];
  }
}
