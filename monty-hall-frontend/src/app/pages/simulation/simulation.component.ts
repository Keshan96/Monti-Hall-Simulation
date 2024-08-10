import { Component, OnInit } from '@angular/core';
import { SimulationService } from '../../core/simulation.service';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Chart, registerables } from 'chart.js';


Chart.register(...registerables);

@Component({
  selector: 'app-simulation',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './simulation.component.html',
  styleUrls: ['./simulation.component.css'] 
})
export class SimulationComponent implements OnInit{
  
  simulationForm1!: FormGroup;
  simulationForm2!: FormGroup;

  result : any;
  wins :number= 0;
  losses :number= 0;

  winsRight :number= 0;
  lossesRight :number= 0;

  private chartLeft: Chart | undefined | any;
  private chartRight: Chart | undefined | any;

  constructor(private simulationService: SimulationService,private fb: FormBuilder) { }

  ngOnInit(): void {
    
    this.simulationForm1 = this.fb.group({
      numberOfGames: [0, [Validators.required, Validators.min(1)]],
      choice: [false],
    });

    this.simulationForm2 = this.fb.group({
      numberOfGames: [0, [Validators.required, Validators.min(1)]],
      choice: [false],
    });
  }

  ngAfterViewInit(): void {
    this.createPieChartLeft();
    this.createPieChartRight();
  }
  
  startSimulation(switchChoice: boolean,formNumber:number) {

    const form = switchChoice ? this.simulationForm1 : this.simulationForm2;
    console.log(form)
    if (form.valid) {
      
      const numberOfGames = form.get('numberOfGames')?.value;
      const choice = form.get('choice')?.value;

      console.log('Proceed with:', {
        switchChoice,
        numberOfGames,
        choice,
      });

      this.simulationService.simulate(numberOfGames, choice).subscribe(data => {
        this.result = data;
        console.log(this.result)
        this.wins = data.wins;
        this.losses = data.losses;
        if(formNumber === 1){
          this.updatePieChartLeft();
        }else{
          this.updatePieChartRight();
        }
       
      });

      // Here, you would call your backend API to process the data.
    } else {
      console.log('Form is invalid');
    }

  }

  createPieChartLeft(): void {
    const ctx = (document.getElementById('myPieChart') as HTMLCanvasElement).getContext('2d');
    this.chartLeft = new Chart(ctx!, {
      type: 'pie',
      data: {
        labels: ['Wins', 'Losses'],
        datasets: [{
          data: [this.wins, this.losses], // Placeholder data
          backgroundColor: ['#36A2EB', '#FF6384'],
        }],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          tooltip: {
            callbacks: {
              label: function(tooltipItem) {
                const data = tooltipItem.chart.data.datasets[0].data;
                const total = data.reduce((a:any, b) => (a + b), 0);
                const value:any = data[tooltipItem.dataIndex];
                const percentage = ((value / total) * 100).toFixed(2);
                return `${tooltipItem.label}: ${percentage}% (${value})`;
              },
            },
          }
        },
      }
    });
  }

  updatePieChartLeft(): void {
    if (this.chartLeft) {
      this.chartLeft.data.datasets[0].data = [this.wins, this.losses]; // Update the data
      this.chartLeft.update(); // Re-render the chart
    }
  }

  createPieChartRight(): void {
    const ctx = (document.getElementById('myPieChart2') as HTMLCanvasElement).getContext('2d');
    this.chartRight = new Chart(ctx!, {
      type: 'pie',
      data: {
        labels: ['Wins', 'Losses'],
        datasets: [{
          data: [this.wins, this.losses], // Placeholder data
          backgroundColor: ['#36A2EB', '#FF6384'],
        }],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          tooltip: {
            callbacks: {
              label: function(tooltipItem) {
                const data = tooltipItem.chart.data.datasets[0].data;
                const total = data.reduce((a:any, b) => (a + b), 0);
                const value:any = data[tooltipItem.dataIndex];
                const percentage = ((value / total) * 100).toFixed(2);
                return `${tooltipItem.label}: ${percentage}% (${value})`;
              },
            },
          }
        },
      },
    });
  }

  updatePieChartRight(): void {
    if (this.chartRight) {
      this.chartRight.data.datasets[0].data = [this.wins, this.losses]; // Update the data
      this.chartRight.update(); // Re-render the chart
    }
  }
}
