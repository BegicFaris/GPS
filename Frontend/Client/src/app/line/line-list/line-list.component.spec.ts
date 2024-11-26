import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LineListComponent } from './line-list.component';  // Adjust the import according to your file structure
import { FormsModule } from '@angular/forms';  // Import FormsModule for ngModel support

describe('LineListComponent', () => {
  let component: LineListComponent;
  let fixture: ComponentFixture<LineListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LineListComponent],  // Replace with the new component
      imports: [FormsModule]  // Import FormsModule for ngModel support in tests
    })
    .compileComponents();

    fixture = TestBed.createComponent(LineListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have no lines initially', () => {
    expect(component.lines.length).toBe(0);  // Initial line count should be 0
  });

  it('should add a new line to the list', () => {
    // Create a new line object and add it to the lines array
    const newLine = {
      id: 1,
      name: 'Line 1',
      startingStationId: 101,
      endingStationId: 102,
      completeDistance: '10 km',
      isActive: true
    };
    component.lines.push(newLine);
    fixture.detectChanges();
    
    // Check that the line was added correctly
    expect(component.lines.length).toBe(1);
    expect(component.lines[0].name).toBe('Line 1');
  });

  it('should select a line for editing', () => {
    const newLine = {
      id: 1,
      name: 'Line 1',
      startingStationId: 101,
      endingStationId: 102,
      completeDistance: '10 km',
      isActive: true
    };
    component.lines.push(newLine);
    fixture.detectChanges();
    
    // Select the line for editing
    component.selectLine(newLine);
    fixture.detectChanges();
    
    // Check that the selected line has been set correctly
    expect(component.selectedLine).toEqual(newLine);
  });

  it('should delete a line from the list', () => {
    const newLine = {
      id: 1,
      name: 'Line 1',
      startingStationId: 101,
      endingStationId: 102,
      completeDistance: '10 km',
      isActive: true
    };
    component.lines.push(newLine);
    fixture.detectChanges();
    
    // Delete the line
    component.deleteLine(newLine.id);
    fixture.detectChanges();
    
    // Check that the line has been removed from the list
    expect(component.lines.length).toBe(0);
  });
});
