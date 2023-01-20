import { Link } from "react-router-dom";

function NotFound() {
    return(
        <>
            <h1>OH N<sup>4</sup>O<sup>4</sup>! You have found the page that does not exist.</h1>
            <p>
                Let's get you back home.
            </p>
            <Link to="/">GO HOME</Link>
        </>
    )
}

export default NotFound;

