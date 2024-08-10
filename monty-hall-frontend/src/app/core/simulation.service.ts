import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

interface SimulationResult {
  totalGames: number;
  wins: number;
  losses: number;
  changedDoor: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class SimulationService {

  private apiUrl = 'https://localhost:7277/api/Simulation/simulate';

  constructor(private http: HttpClient) { }

  simulate(numberOfGames: any, changeDoor: boolean | any): Observable<SimulationResult> {
    return this.http.get<SimulationResult>(`${this.apiUrl}?numberOfGames=${numberOfGames}&changeDoor=${changeDoor}`);
  }
}
