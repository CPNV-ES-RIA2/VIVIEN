<script setup>
defineProps({
    results: {
        type: Object,
        required: true
    }
})
</script>
<template>
    <table>
        <thead>
            <th>Description</th>
            <th>Value</th>
        </thead>
        <tbody>
            <tr v-for="result in results">
                <td>{{ result.description }}</td>
                <td>{{ result.value }}</td>
            </tr>
        </tbody>
    </table>
</template>