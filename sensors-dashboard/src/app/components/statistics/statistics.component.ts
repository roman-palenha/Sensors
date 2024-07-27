import { Component, OnInit } from '@angular/core';
import { SensorService } from '../../services/sensor.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css'],
})
export class StatisticsComponent implements OnInit {
  startDate!: Date;
  startTime!: string;
  endDate!: Date;
  endTime!: string;
  groupNames = [
    'alpha',
    'beta',
    'gamma',
    'delta',
    'epsilon',
    'zeta',
    'eta',
    'theta',
    'iota',
  ];
  averageTemperature: number | undefined;
  topFishSpecies: any[] = [];
  groupStatistics: any[] = [];

  constructor(private sensorService: SensorService) {}

  ngOnInit(): void {
    this.endDate = new Date();
    this.endTime = '23:59';
    this.startDate = new Date();
    this.startDate.setDate(this.endDate.getDate() - 7);
    this.startTime = '00:00';

    this.updateStatistics();
  }

  updateStatistics(): void {
    const startDateTime = this.combineDateAndTime(
      this.startDate,
      this.startTime
    );
    const endDateTime = this.combineDateAndTime(this.endDate, this.endTime);

    this.sensorService
      .getTotalTemperatureAverage(startDateTime, endDateTime)
      .subscribe((data) => {
        this.averageTemperature = data.averageWaterTemperature;
      });

    this.sensorService
      .getTopFishSpecies(5, startDateTime, endDateTime)
      .subscribe((data) => {
        this.topFishSpecies = data;
      });

    this.groupStatistics = [];
    this.groupNames.forEach((group) => {
      this.sensorService
        .getGroupTemperatureAverage(group, startDateTime, endDateTime)
        .subscribe((avgTemp) => {
          this.sensorService
            .getGroupTopFishSpecies(group, 3, startDateTime, endDateTime)
            .subscribe((topFish) => {
              this.groupStatistics.push({
                name: group,
                averageTemperature: avgTemp.averageWaterTemperature,
                topFishSpecies: topFish,
              });
            });
        });
    });
  }

  combineDateAndTime(date: Date, time: string): Date {
    const [hours, minutes] = time.split(':').map(Number);
    const combinedDate = new Date(date);
    combinedDate.setHours(hours, minutes);

    const utcDate = new Date(
      combinedDate.getTime() - combinedDate.getTimezoneOffset() * 60000
    );
    return utcDate;
  }
}
