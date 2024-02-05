<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <h1>Cinema Ticket</h1>
    <p>Cinema Ticket is a full-stack web application designed for cinema enthusiasts and administrators. It allows users to browse and purchase cinema tickets for upcoming movies, while providing administrators with tools for managing showtimes, movie listings, and ticket sales. The project is built using React for the frontend and ASP.NET Core for the backend, ensuring a responsive, scalable, and secure application.</p>
    <h2>Features</h2>
    <ul>
        <li><strong>User-Friendly Interface:</strong> Easy navigation and interaction for purchasing tickets and viewing movie details.</li>
        <li><strong>Comprehensive Movie Catalog:</strong> Includes detailed pages for each movie, featuring descriptions, showtimes, and ticket purchasing options.</li>
        <li><strong>Basket Functionality:</strong> Users can add tickets to their basket, review their selections, and proceed to checkout.</li>
        <li><strong>Secure User Authentication:</strong> Supports user registration, login, and profile management.</li>
        <li><strong>Admin Management:</strong> Allows administrators to add or update movie listings, manage showtimes, and view sales reports.</li>
    </ul>
    <h2>Technology Stack</h2>
    <ul>
        <li><strong>Frontend:</strong> React (TypeScript), Redux for state management, and Material UI for component styling.</li>
        <li><strong>Backend:</strong> ASP.NET Core, Entity Framework Core for data access, and SQL Server as the database.</li>
        <li><strong>Payment Processing:</strong> Integrated with Stripe for secure online payments.</li>
        <li><strong>Other Tools:</strong> Docker for containerization and deployment, GitHub Actions for CI/CD.</li>
    </ul>
    <h2>Getting Started</h2>
    <h3>Prerequisites</h3>
    <ul>
        <li>Node.js</li>
        <li>.NET 5.0 SDK</li>
        <li>SQL Server</li>
    </ul>
    <h3>Setup</h3>
    <ol>
        <li><strong>Clone the repository</strong>
            <pre><code>git clone https://github.com/AliShahbazi81/Cinema-Ticket.git
cd Cinema-Ticket</code></pre>
</li>
<li><strong>Set up the backend</strong>
<p>Navigate to the <code>e-Commerce</code> directory and restore the .NET dependencies.</p>
<pre><code>cd e-Commerce
dotnet restore</code></pre>
<p>Update the database with the initial migration (ensure SQL Server is running).</p>
<pre><code>dotnet ef database update</code></pre>
<p>Start the backend server.</p>
<pre><code>dotnet run</code></pre>
</li>
<li><strong>Set up the frontend</strong>
<p>Navigate to the <code>client</code> directory and install the npm packages.</p>
<pre><code>cd ../client
npm install</code></pre>
<p>Start the React application.</p>
<pre><code>npm start</code></pre>
<p>The application should now be running locally, with the frontend accessible on <code>http://localhost:3000</code> and the backend on <code>http://localhost:5000</code>.</p>
</li>
</ol>
    <h2>Contributing</h2>
    <p>We welcome contributions to the Cinema Ticket project. Please feel free to submit issues or pull requests through GitHub for any bugs, features, or enhancements.</p>
    <h2>License</h2>
    <p>This project is licensed under the MIT License - see the LICENSE file for details.</p>
</body>
</html>
