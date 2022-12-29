import { Box, Typography } from "@mui/material";
import Slider from "react-slick";

export default function HomePage() {
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
  };
  return (
    <>
      <Slider {...settings}>
        <div>
          <img
            src="https://i.pinimg.com/originals/98/ca/97/98ca978b5caae38cb7a66ad3d3bd5372.jpg"
            alt=""
            style={{ display: "block", width: "100%", maxHeight: "500" }}
          />
        </div>
        <div>
          <img
            src="https://i.pinimg.com/originals/08/62/e7/0862e7a3c52d2d7bda9010bf2117689f.jpg"
            alt=""
            style={{ display: "block", width: "100%", maxHeight: "500" }}
          />
        </div>
        <div>
          <img
            src="https://www.pixelstalk.net/wp-content/uploads/2016/07/1080p-HD-Image-Nature-Desktop.jpg"
            alt=""
            style={{ display: "block", width: "100%", maxHeight: "500" }}
          />
        </div>
      </Slider>
      <Box display="flex" justifyContent="center" sx={{ p: 4 }}></Box>
    </>
  );
}
