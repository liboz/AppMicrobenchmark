package main

import (
	"math/rand"
	"net/http"
	"time"

	"github.com/labstack/echo/v4"
	"github.com/labstack/echo/v4/middleware"
)

type User struct {
	Name  string `json:"name"`
	Index int    `json:"index"`
}

type BenchmarkRequest struct {
	Users []User `json:"users"`
}

type BenchmarkResponse struct {
	MyInt int    `json:"myInt"`
	Users []User `json:"users"`
}

func main() {
	// Echo instance
	e := echo.New()

	// Middleware
	e.Use(middleware.Logger())
	e.Use(middleware.Recover())
	e.Use(middleware.CORSWithConfig(middleware.DefaultCORSConfig))

	// Route => handler
	e.POST("/benchmark", func(c echo.Context) error {
		r := rand.New(rand.NewSource(time.Now().Unix()))
		data := BenchmarkRequest{}
		if err := c.Bind(&data); err != nil {
			return err
		}
		resultMap := make(map[int]bool)
		count := 0

		for i := 0; i < 100000; i++ {
			firstIndex := r.Intn(len(data.Users))
			secondIndex := r.Intn(len(data.Users))
			first := data.Users[firstIndex]
			second := data.Users[secondIndex]
			if first.Name > second.Name {
				count += 1
				resultMap[firstIndex] = true
			} else {
				count -= 1
				resultMap[firstIndex] = false
			}
		}

		response := BenchmarkResponse{}
		response.MyInt = count
		var result []User

		i := 0
		for k, val := range resultMap {
			if val {
				result = append(result, data.Users[k])
			}
			i++
		}
		response.Users = result

		return c.JSON(http.StatusOK, response)
	})

	// Start server
	e.Logger.Fatal(e.Start(":8080"))
}
