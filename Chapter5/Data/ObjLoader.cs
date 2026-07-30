public class ObjLoader
{
    private IEnumerable<string>? lines;

    public ObjLoader(string filename)
    {
        if (Load(filename)) return;
        if (Load(Path.Combine("objs", filename))) return;
        if (Load(Path.Combine("../objs", filename))) return;
        if (Load(Path.Combine("../../objs", filename))) return;
        if (Load(Path.Combine("../../../objs", filename))) return;

        Console.Error.WriteLine($"이미지 경로 또는 이름 오류");
    }

    private bool Load(string filename)
    {
        if (!File.Exists(filename)) return false;
        try
        {
            lines = File.ReadLines(filename);
            if (lines.Count() <= 0) return false;
            else if (lines == null) return false;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void LoadToWorld(Material mat, HittableList world)
    {
        List<Vector3> vertices = new List<Vector3>();
        
        foreach (string line in lines)
        {
            string trimmed = line.Trim();

            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                continue;

            string[] parts = trimmed.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) continue;

            // 정점(Vertex) 파싱
            if (parts[0] == "v" && parts.Length >= 4)
            {
                float x = float.Parse(parts[1]);
                float y = float.Parse(parts[2]);
                float z = float.Parse(parts[3]);
                vertices.Add(new Vector3(x, y, z));
            }
            // 면(Face) 파싱
            else if (parts[0] == "f" && parts.Length >= 4)
            {
                // OBJ 파일은 1부터 시작하므로 인덱스 보정이 필요함
                int i0 = ParseVertexIndex(parts[1], vertices.Count);
                int i1 = ParseVertexIndex(parts[2], vertices.Count);

                // 팬 분할 적용
                for (int i = 2; i < parts.Length - 1; i++)
                {
                    int i2 = ParseVertexIndex(parts[i + 1], vertices.Count);
                    world.add(new Triangle(vertices[i0], vertices[i1], vertices[i2], mat));

                    i1 = i2; // 다음 삼각형을 위한 인덱스 시프트
                }
            }
        }
    }

    private int ParseVertexIndex(string token, int vertexCount)
    {
        // OBJ 면 형식은 v, v/vt, v/vt/vn, v//vn 등 다양하므로 첫 번째 슬래시 앞의 값만 추출
        string indexPart = token.Split('/')[0];
        int idx = int.Parse(indexPart);

        // 음수 인덱스 지원 (마지막 정점 기준 상대 역순)
        if (idx < 0)
        {
            idx = vertexCount + idx + 1;
        }

        return idx - 1; // 1 기반 인덱스를 0 기반 C# 인덱스로 변환
    }
}