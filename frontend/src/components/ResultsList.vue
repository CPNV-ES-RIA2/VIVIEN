<script setup>
import {watch} from 'vue'

const props = defineProps({
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
                <td>{{ result.name }}</td>
                <td>{{ result.confidence }}</td>
            </tr>
        </tbody>
    </table>
</template>