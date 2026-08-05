# 코드로 구현하는 3D 그래픽스 메쉬 생성부터 레이트레이싱까지

## Getting Started

### Prerequisites
- Unity verison 6000.0.74f1
- .NET 9.0 이상

### Installation
1. Cloning Project
   ```git
   git clone https://github.com/zkco/CustomRendering
   ```
2. Build Project File

   Chapter 1 ~ 2
      1. [Unity Download Archive](https://unity.com/kr/releases/editor/archive) 6.000.0.74f1 버전 다운로드
      2. 3D URP 프로젝트 생성
      3. Chapter [1,2] 코드와 에셋을 사용
      
   Chapter 3 ~ 5
      1. Visual Studio 새 프로젝트 생성
      2. 프로젝트 템플릿 : 콘솔 앱
      3. 프레임워크를 .NET 9.0 이상으로 사용
      4. Github 내부의 Chapter [3,4,5] 폴더의 코드를 사용하여 빌드

### Running
Chapter 1 ~ 2
```
   유니티 실행 후 빈 오브젝트 생성. 다음 c#으로 컴포넌트 작성 후 빈 오브젝트에 이식
```

Chapter 3 ~ 5
```bash
   dotnet run {buildfile}.dll
```
   또는 Visual Studio에서 Program.cs 컴파일


## Learning with UNITY
Unity를 통합 개발 환경(IDE)으로 인식하고 사용하기
-----------------------------------------
### Chapter 1. 메쉬의 이해와 절차적 생성
- 정점(Vertex)과 인덱스(Index) 버퍼의 이해
- 큐브 메쉬의 절차적 생성하기
- 구(Sphere) 메쉬의 절차적 생성하기
- 큐브 메쉬의 텍스처 매핑과 동적 제어
- 구(Sphere) 메쉬의 텍스처 매핑과 최적화하기

### Chapter 2. 3D 데이터 로더 구현
- 파일 입출력 및 파싱(parsing) 알고리즘 구현하기(OBJ)
- 파일 입출력 및 파싱(parsing) 알고리즘 구현하기(STL)
- 외부 3D 데이터를 유니티 메쉬로 변환 및 시각화



## Learning with C# (.Net 10.0)
.Net 10.0 C#을 사용하여 레이트레이싱 엔진을 제작 후 PPM 파일로 렌더링하기
-----------------------------------------
### Chapter 3. 레이트레이싱 엔진 기초
- C#을 이용한 PPM 이미지 파일을 직접 생성하기
- Vector3, Ray, Color 등의 구조체를 직접 작성하고 필요한 메서드를 구현하기
- 가상의 구를 생성하고 Ray와 Object의 접점은 근의 공식으로 구현하기
- 난반사 재질의 구현방법과 알고리즘에 대하여 학습하기

### Chapter 4. 고급 렌더링
- Material 구조체를 생성하여 다양한 재질을 구현하기
- 반사, 투명, 굴절에 필요한 현실 물리법칙의 공식을 C#으로 구현하기
- 카메라의 이동, 회전과 초점을 구현하여 자유로운 촬영 시점 구현하기

### Chapter 5. 레이트레이싱 오브젝트
- Sphere 외의 Square, Triangle 의 오브젝트를 생성하기
- Texture Mapping을 통한 이미지를 오브젝트에 적용하기
- Triangle 오브젝트 알고리즘 기반 OBJ 파일 로더를 통한 오브젝트 생성하기


